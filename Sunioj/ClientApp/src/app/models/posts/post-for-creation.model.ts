import { Tag } from "./tag.model";

export class PostForCreation {
    title: string;
    summary: string;
    content: string;
    tags: Tag[];
    isDraft: boolean;
    thumbnail: File;
}
