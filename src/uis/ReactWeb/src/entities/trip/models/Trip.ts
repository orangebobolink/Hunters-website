import {TripParticipant} from '@/entities/trip/models/TripParticipant.ts';
import {Permission} from '@/entities/permision/Permision.ts';

export interface Trip {
    id?: string;
    number: string;
    permissionId: string;
    permission?: Permission | null;
    specialConditions: string;
    eventDate?: Date;
    tripParticipants?: TripParticipant[];
    returnedDate?: Date;
    price: number;
    status: string;
}