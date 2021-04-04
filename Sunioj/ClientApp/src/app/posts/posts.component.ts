import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { Post } from '../models/posts/post.model';
import { PostsService } from '../services/posts.service';

@Component({
    selector: 'app-posts',
    templateUrl: 'posts.component.html'
})
export class PostsComponent implements OnInit, OnDestroy {

    isLoading: boolean = true;

    dtOptions: DataTables.Settings = {
        lengthChange: false,
        order: [0, 'desc']
    };

    posts: Post[] = [];
    postsTrigger: Subject<any> = new Subject<any>();

    constructor(private postsService: PostsService) {

    }

    ngOnInit() {
        this.postsService.getAll()
            .subscribe(posts => {
                this.posts = posts;
                this.postsTrigger.next();
                this.isLoading = false;
            });
    }

    ngOnDestroy(): void {
        this.postsTrigger.unsubscribe();
    }
}