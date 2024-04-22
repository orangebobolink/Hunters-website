import {User} from '@/entities/user/User.ts';

export interface HuntingLicense {
    id: string;
    userId: string;
    user?: User | null;
    licenseNumber: string;
}