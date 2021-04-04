import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Message } from '../models/messages/message.model';
import { MessageForCreation } from '../models/messages/message-for-creation.model';

@Injectable()
export class MessagesService {

    constructor(private httpClient: HttpClient) {

    }

    getAll(): Observable<Message[]> {
        return this.httpClient.get<Message[]>('messages');
    }

    create(message: MessageForCreation): Observable<Message> {
        return this.httpClient.post<Message>('messages', message);
    }

    getById(id: number): Observable<Message> {
        return this.httpClient.get<Message>(`messages/${id}`);
    }

    delete(id: number) {
        return this.httpClient.delete(`messages/${id}`);
    }
}