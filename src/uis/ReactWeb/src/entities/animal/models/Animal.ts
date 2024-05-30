import {HuntingSeason} from '@/entities/animal/models/HuntingSeason.ts';

export interface Animal {
    id?: string;
    name: string;
    type: string;
    description: string;
    imageUrl: string;
    huntingSeasons?: HuntingSeason[]
}