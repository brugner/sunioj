import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Setting } from '../models/settings/setting.model';
import { SettingForUpdate } from '../models/settings/setting-for-update.model';
import { ImageSettingForUpdate } from '../models/settings/image-setting-for-update.model';
import { SESSION_STORAGE, StorageService, StorageTranscoders } from 'ngx-webstorage-service';
import { map } from 'rxjs/operators';

@Injectable()
export class SettingsService {

    private readonly cacheKey = 'sj-settings';

    constructor(
        @Inject(SESSION_STORAGE) private storageService: StorageService,
        private httpClient: HttpClient) {

    }

    async getAll(): Promise<Setting[]> {
        if (this.storageService.has(this.cacheKey)) {
            const settings = this.storageService.get<Setting[]>(this.cacheKey, StorageTranscoders.JSON);
            return Promise.resolve<Setting[]>(settings);
        }

        return this.httpClient.get<Setting[]>('settings')
            .pipe(map((settings: Setting[]) => {
                this.storageService.set(this.cacheKey, settings);
                return settings;
            })).toPromise();
    }

    update(settingsForUpdate: SettingForUpdate[]): Observable<Setting[]> {
        return this.httpClient.put<Setting[]>('settings', settingsForUpdate)
            .pipe(map((settings: Setting[]) => {
                this.storageService.set(this.cacheKey, settings);
                return settings;
            }));
    }

    updateImage(imageSettingForUpdate: ImageSettingForUpdate): Observable<Setting> {
        const data = new FormData();
        data.append('name', imageSettingForUpdate.name);
        data.append('image', imageSettingForUpdate.image, imageSettingForUpdate.image.name);

        return this.httpClient.put<Setting>('settings/image', data)
            .pipe(map((setting: Setting) => {
                const settings = this.storageService.get<Setting[]>(this.cacheKey, StorageTranscoders.JSON);
                settings.find(x => x.name === imageSettingForUpdate.name).value = setting.value;

                this.storageService.set(this.cacheKey, settings);

                return setting;
            }));
    }

    async getSetting(name: string): Promise<string> {
        if (this.storageService.has(this.cacheKey)) {
            const settings = this.storageService.get<Setting[]>(this.cacheKey, StorageTranscoders.JSON);
            return settings.find(x => x.name === name).value;
        } else {
            const settings = await this.httpClient.get<Setting[]>('settings').toPromise()
            this.storageService.set(this.cacheKey, settings);
            return settings.find(x => x.name === name).value;
        }
    }
}