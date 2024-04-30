import {HuntingSeason} from "@/entities/huntinSeason/HuntingSeason.ts";

export interface Animal {
    id?: string;
    name: string;
    type: string;
    description: string;
    imageUrl: string;
    huntingSeasons?: HuntingSeason[]
}