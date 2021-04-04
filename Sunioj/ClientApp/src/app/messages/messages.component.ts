import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { Message } from '../models/messages/message.model';
import { MessagesService } from '../services/messages.service';

@Component({
    selector: 'app-messages',
    templateUrl: 'messages.component.html'
})
export class MessagesComponent implements OnInit, OnDestroy {

    isLoading: boolean = true;

    dtOptions: DataTables.Settings = {
        lengthChange: false,
        order: [0, 'desc']
    };

    messages: Message[] = [];
    messagesTrigger: Subject<any> = new Subject<any>();

    constructor(private messagesService: MessagesService) {

    }

    ngOnInit(): void {
        this.messagesService.getAll()
            .subscribe(messages => {
                this.messages = messages;
                this.messagesTrigger.next();
                this.isLoading = false;
            });
    }

    ngOnDestroy(): void {
        this.messagesTrigger.unsubscribe();
    }
}