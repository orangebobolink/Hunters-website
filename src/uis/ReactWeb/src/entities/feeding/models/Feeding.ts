import {FeedingProduct} from '@/entities/feeding/models/FeedingProduct.ts';
import {Land} from '@/entities/land/Land.ts';

export interface Feeding {
    id?:string;
    number: string;
    feedingDate: Date;
    issuedId: string;
    receivedId: string;
    receivedDate: Date;
    products: FeedingProduct[];
    landId: string;
    land:Land;
    status: string;
}