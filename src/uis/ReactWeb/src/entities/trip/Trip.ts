import {User} from '@/entities/user/User.ts';
import {TripParticipant} from '@/entities/tripParticipant/TripParticipant.ts';
import {Permission} from '@/entities/permision/Permision.ts';

export interface Trip {
    id: string;
    fromDate: Date;
    toDate: Date;
    number: string;
    permissionId: string;
    permission?: Permission | null;
    specialConditions: string;
    issuedId: string;
    issued?: User | null;
    receivedId: string;
    received?: User | null;
    receivedDate: Date;
    eventDate: Date;
    tripParticipants: TripParticipant[];
    returnedDate: Date;
    isReturned: boolean;
    buyerId: string;
    buyer?: User | null;
    amountOfFee: number;
}