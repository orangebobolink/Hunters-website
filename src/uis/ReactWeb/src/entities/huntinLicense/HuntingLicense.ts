import {User} from '@/entities/user/models/User.ts';

export interface HuntingLicense {
    id?: string;
    userId: string;
    user?: User | null;
    licenseNumber: string;
    isPaid?: boolean;
}