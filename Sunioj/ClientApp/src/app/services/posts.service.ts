import { Tag } from '../models/posts/tag.model';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PublishedPost } from '../models/posts/published-post.model';
import { Post } from '../models/posts/post.model';
import { PostForUpdate } from '../models/posts/post-for-update.model';
import { PostForCreation } from '../models/posts/post-for-creation.model';

@Injectable()
export class PostsService {

    constructor(private httpClient: HttpClient) {

    }

    getAllPublished(): Observable<PublishedPost[]> {
        return this.httpClient.get<PublishedPost[]>('posts');
    }

    getAll(): Observable<Post[]> {
        return this.httpClient.get<Post[]>('posts/list');
    }

    getWithTag(tag: string): Observable<PublishedPost[]> {
        return this.httpClient.get<PublishedPost[]>(`posts/tag/${tag}`);
    }

    getAllTags(): Observable<Tag[]> {
        return this.httpClient.get<Tag[]>('posts/tags');
    }

    getBySlug(slug: string): Observable<PublishedPost> {
        return this.httpClient.get<PublishedPost>(`posts/slug/${slug}`);
    }

    getById(id: number): Observable<PublishedPost> {
        return this.httpClient.get<PublishedPost>(`posts/${id}`);
    }

    delete(id: number): Observable<any> {
        return this.httpClient.delete(`posts/${id}`);
    }

    create(post: PostForCreation): Observable<PublishedPost> {
        const formData = this.getFormDataFromPost(post);

        return this.httpClient.post<PublishedPost>('posts', formData);
    }

    update(id: number, post: PostForUpdate): Observable<PublishedPost> {
        const formData = this.getFormDataFromPost(post);

        return this.httpClient.put<PublishedPost>(`posts/${id}`, formData);
    }

    private getFormDataFromPost(post: PostForUpdate | PostForCreation): FormData {
        const formData = new FormData();
        formData.append('title', post.title);
        formData.append('summary', post.summary);
        formData.append('content', post.content);
        formData.append('isDraft', String(post.isDraft));

        for (var i = 0; i < post.tags.length; i++) {
            formData.append('tags[' + i + '].id', post.tags[i].id.toString());
            formData.append('tags[' + i + '].name', post.tags[i].name);
        }

        if (post.thumbnail) {
            formData.append('thumbnail', post.thumbnail, post.thumbnail.name);
        }

        return formData;
    }
}