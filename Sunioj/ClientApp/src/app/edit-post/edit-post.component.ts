import { SettingsService } from './../services/settings.service';
import { PostForCreation } from './../models/posts/post-for-creation.model';
import { Tag } from '../models/posts/tag.model';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PublishedPost } from '../models/posts/published-post.model';
import { PostsService } from '../services/posts.service';
import { PostForUpdate } from '../models/posts/post-for-update.model';

@Component({
  selector: 'app-edit-post',
  templateUrl: './edit-post.component.html',
  styleUrls: ['./edit-post.component.css']
})
export class EditPostComponent implements OnInit {

  siteName: string;
  post: PublishedPost;
  selectedTags: Tag[] = [];
  allTags: Tag[] = [];
  postForm: FormGroup;
  isSubmitting: boolean = false;
  isLoading: boolean = true;
  postThumbnail: File;
  postThumbnailUrl: string = '';
  isNew: boolean = false;

  public editorConfig = {
    height: 500,
    menubar: false,
    plugins: [
      'advlist autolink lists link image charmap print preview anchor',
      'searchreplace visualblocks code fullscreen',
      'insertdatetime media table paste code help wordcount'
    ],
    toolbar:
      'undo redo | formatselect | bold italic backcolor | \
  alignleft aligncenter alignright alignjustify | \
  bullist numlist outdent indent | removeformat | help',
    paste_data_images: true
  };

  constructor(
    private postsService: PostsService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private settingsService: SettingsService) {

  }

  async ngOnInit(): Promise<void> {
    this.siteName = await this.settingsService.getSetting('site.name');

    this.postsService.getAllTags()
      .subscribe(tags => this.allTags = tags);

    this.buildPostForm();

    const id = Number.parseInt(this.route.snapshot.paramMap.get('id'));

    if (isNaN(id)) {
      this.isLoading = false;
      this.isNew = true;
      this.cleanPostForm();
    }
    else {
      this.loadPost(id);
    }
  }

  selectEvent(item: Tag) {
    this.AddTag(item);
  }

  onChangeSearch(value: string) {
    this.tag.setValue(value.replace(/[^a-zA-Z0-9\-]/g, ''))
  }

  onKeyDownEvent(event: any) {
    event.stopPropagation();
    this.AddTag(new Tag(0, this.tag.value));
  }

  removeTag(tag: Tag): void {
    const index = this.selectedTags.findIndex(x => x.name === tag.name);

    this.selectedTags.splice(index, 1);
  }

  onImageChanged(event: any) {
    if (event.target.files.length === 0) {
      return;
    }

    const file: File = event.target.files[0];
    const reader = new FileReader();

    reader.addEventListener('load', (event: any) => {
      this.postThumbnail = file;
      this.postThumbnailUrl = event.target.result;

    });

    reader.readAsDataURL(file);
  }

  submitForm(): void {
    this.isSubmitting = true;

    if (this.isNew) {
      const post = this.getPostForCreation();

      this.postsService.create(post)
        .subscribe(response => {
          this.isSubmitting = false;
          this.toastrService.success('Post created', this.siteName);
          this.cleanPostForm();
          this.router.navigate(["blog", "post", response.slug]);
        }, () => {
          this.isSubmitting = false;
        });
    }
    else {
      const post = this.getPostForUpdate();

      this.postsService.update(this.id.value, post)
        .subscribe(post => {
          this.isSubmitting = false;
          this.toastrService.success('Post updated', this.siteName);
          this.cleanPostForm();
          this.router.navigate(["blog", "post", post.slug]);
        }, () => {
          this.isSubmitting = false;
        });
    }
  }

  deletePost() {
    if (confirm("Careful, deletion is permanent. Are you sure you want to delete this post?")) {
      this.postsService.delete(this.id.value).subscribe(() => {
        this.toastrService.success('Post deleted', this.siteName);
        this.router.navigateByUrl('/posts');
      });
    };
  }

  private loadPost(id: number) {

    this.postsService.getById(id)
      .subscribe(post => {
        this.setFormFromPost(post);
        this.isLoading = false;
      });
  }

  private buildPostForm() {
    this.postForm = this.formBuilder.group({
      id: new FormControl(''),
      title: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(100)]),
      summary: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(200)]),
      content: new FormControl('', [Validators.required]),
      tag: new FormControl(''),
      isDraft: new FormControl(false, [Validators.required])
    });
  }

  private cleanPostForm(): void {
    this.id.setValue('');
    this.title.setValue('');
    this.summary.setValue('');
    this.content.setValue('');
    this.selectedTags = [];
    this.isDraft.setValue(false);
    this.postThumbnail = null;
    this.postThumbnailUrl = '';
  }

  private setFormFromPost(post: PublishedPost) {
    this.id.setValue(post.id);
    this.title.setValue(post.title);
    this.summary.setValue(post.summary);
    this.content.setValue(post.content);
    this.selectedTags = post.tags;
    this.isDraft.setValue(post.isDraft);
    this.postThumbnailUrl = post.thumbnail ? '..\\' + post.thumbnail : '..\\images\\posts\\defaultThumbnail.jpg';
  }

  private getPostForCreation(): PostForCreation {
    let post = new PostForCreation();
    post.title = this.title.value;
    post.summary = this.summary.value;
    post.content = this.content.value;
    post.tags = this.selectedTags;
    post.isDraft = this.isDraft.value;
    post.thumbnail = this.postThumbnail;

    return post;
  }

  private getPostForUpdate(): PostForUpdate {
    let post = new PostForUpdate();
    post.title = this.title.value;
    post.summary = this.summary.value;
    post.content = this.content.value;
    post.tags = this.selectedTags;
    post.isDraft = this.isDraft.value;
    post.thumbnail = this.postThumbnail;

    return post;
  }

  private AddTag(item: Tag) {
    if (!this.selectedTags.some(x => x.name === item.name)) {
      this.selectedTags.push(item);
      this.tag.setValue('');
    }
  }

  get id() {
    return this.postForm.get('id');
  }

  get title() {
    return this.postForm.get('title');
  }

  get summary() {
    return this.postForm.get('summary');
  }

  get content() {
    return this.postForm.get('content');
  }

  get tag() {
    return this.postForm.get('tag');
  }

  get isDraft() {
    return this.postForm.get('isDraft');
  }
}
