import {Animal} from '@/entities/animal/Animal.ts';
import {User} from '@/entities/user/User.ts';
import {Coupon} from '@/entities/coupon/Coupon.ts';

export interface Permission  {
    id: string;
    fromDate: Date;
    toDate: Date;
    number: string;
    animalId: string;
    animal?: Animal | null;
    issuedId: string;
    issued?: User | null;
    receivedId: string;
    received?: User | null;
    coupons: Coupon[];
}