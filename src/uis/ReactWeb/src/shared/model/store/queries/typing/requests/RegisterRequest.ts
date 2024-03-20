import {Sex} from '@/shared/model/store/queries/typing/requests/Sex.ts';

export interface RegisterRequest {
    email: string,
    phoneNumber: string,
    password: string,
    firstName: string,
    lastName: string,
    dateOfBirth: Date,
    sex: Sex,
}