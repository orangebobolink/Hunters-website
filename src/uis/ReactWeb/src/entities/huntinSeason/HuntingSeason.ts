export interface HuntingSeason {
    id: string;
    animalId: string;
    startDate: Date;
    endDate: Date;
    wayOfHunting: string;
    weapon: string;
    note: string;
}