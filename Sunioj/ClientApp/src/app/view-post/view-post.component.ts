import { Tag } from '../models/posts/tag.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PublishedPost } from '../models/posts/published-post.model';
import { AuthService } from '../services/auth.service';
import { PostsService } from '../services/posts.service';
import { SettingsService } from '../services/settings.service';

@Component({
  selector: 'app-view-post',
  templateUrl: './view-post.component.html'
})
export class ViewPostComponent implements OnInit {

  siteName: string;
  post: PublishedPost = new PublishedPost();
  allTags: Tag[] = [];
  isLoading: boolean = true;
  pageId: string;

  constructor(
    private postsService: PostsService,
    private route: ActivatedRoute,
    public authService: AuthService,
    private toastrService: ToastrService,
    private router: Router,
    private settingsService: SettingsService) {

  }

  async ngOnInit(): Promise<void> {
    this.siteName = await this.settingsService.getSetting('site.name');

    const slug = this.route.snapshot.paramMap.get('slug');

    this.postsService.getBySlug(slug)
      .subscribe(response => {
        this.post = response;
        this.isLoading = false;
        this.pageId = 'blog/post/' + this.post.slug;
      });

    this.postsService.getAllTags()
      .subscribe(tags => {
        this.allTags = tags;
      });
  }

  deletePost() {
    if (confirm("Careful, deletion is permanent. Are you sure you want to delete this post?")) {
      this.postsService.delete(this.post.id).subscribe(() => {
        this.toastrService.success('Post deleted', this.siteName);
        this.router.navigateByUrl('/posts');
      });
    };
  }
}
