import {Animal} from '@/entities/animal/models/Animal.ts';
import {User} from '@/entities/user/models/User.ts';
import {Coupon} from '@/entities/feeding/models/Coupon.ts';
import {Land} from '@/entities/land/Land.ts';

export interface Permission  {
    id?: string;
    fromDate: Date;
    toDate: Date;
    number: string;
    animalId: string;
    animal?: Animal | null;
    issuedId: string;
    issued?: User | null;
    receivedId: string;
    received?: User | null;
    status: string,
    coupons?: Coupon[];
    numberOfCoupons?: number;
    landId:string,
    land:Land
}