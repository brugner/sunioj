import { SettingsService } from './../services/settings.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { PublishedPost } from '../models/posts/published-post.model';
import { AuthService } from '../services/auth.service';
import { PostsService } from '../services/posts.service';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {

  blogTitle: string;
  blogSubtitle: string;
  posts: PublishedPost[];
  isLoading: boolean = true;
  selectedTag: string = null;

  constructor(
    private postsService: PostsService,
    public authService: AuthService,
    private route: ActivatedRoute,
    private settingsService: SettingsService) {

  }

  async ngOnInit(): Promise<void> {
    this.blogTitle = await this.settingsService.getSetting('blog.title');
    this.blogSubtitle = await this.settingsService.getSetting('blog.subtitle');

    this.route.paramMap.subscribe(paramMap => {
      this.selectedTag = paramMap.get('tag');

      if (this.selectedTag === null) {
        this.postsService.getAllPublished()
          .subscribe(posts => {
            this.posts = posts;
            this.isLoading = false;
          });
      } else {
        this.postsService.getWithTag(this.selectedTag)
          .subscribe(posts => {
            this.posts = posts;
            this.isLoading = false;
          });
      }
    });
  }
}
