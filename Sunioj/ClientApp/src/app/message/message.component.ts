import { SettingsService } from './../services/settings.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from './../models/messages/message.model';
import { ActivatedRoute, Router } from '@angular/router';
import { MessagesService } from './../services/messages.service';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-message',
    templateUrl: 'message.component.html'
})
export class MessageComponent implements OnInit {

    siteName: string;
    message: Message = new Message();
    isLoading: boolean = true;

    constructor(
        private route: ActivatedRoute,
        private messagesService: MessagesService,
        private toastrService: ToastrService,
        private router: Router,
        private settingsService: SettingsService) {

    }

    async ngOnInit(): Promise<void> {
        this.siteName = await this.settingsService.getSetting('site.name');
        const id = Number.parseInt(this.route.snapshot.paramMap.get('id'));

        this.messagesService.getById(id)
            .subscribe((message: Message) => {
                this.message = message;
                this.isLoading = false;
            });
    }

    deleteMessage(): void {
        if (confirm("Are you sure you want to delete this message?")) {
            this.messagesService.delete(this.message.id).subscribe(() => {
                this.toastrService.success('Message deleted', this.siteName);
                this.router.navigateByUrl('/messages');
            });
        };
    }
}