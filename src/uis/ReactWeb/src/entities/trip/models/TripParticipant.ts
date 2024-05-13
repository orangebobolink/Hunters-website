import {User} from '@/entities/user/User.ts';
import {Trip} from '@/entities/trip/Trip.ts';
import {HuntingLicense} from '@/entities/huntinLicense/HuntingLicense.ts';

export interface TripParticipant {
    id?: string;
    participantId: string;
    participant?: User | null;
    huntingLicenseId: string;
    huntingLicense?: HuntingLicense | null;
    tripId: string;
    trip?: Trip | null;
}