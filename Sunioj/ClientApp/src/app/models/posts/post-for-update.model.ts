import { Tag } from "./tag.model";

export class PostForUpdate {
    title: string;
    summary: string;
    content: string;
    tags: Tag[];
    isDraft: boolean;
    thumbnail: File;
}
