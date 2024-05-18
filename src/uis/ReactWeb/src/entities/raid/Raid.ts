import {Land} from '@/entities/land/Land.ts';
import {User} from '@/entities/user/models/User.ts';

export interface Raid {
    id?:string;
    exitTime: Date;
    returnedTime: Date;
    participants: User[];
    note:string;
    landId: string;
    land:Land;
    status: string;
}