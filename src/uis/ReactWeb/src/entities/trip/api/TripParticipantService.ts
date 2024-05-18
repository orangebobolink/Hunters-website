import {apiMap} from '@/shared/const';
import {axiosInstance} from '@/shared/api/axiosInstance.ts';
import {TripParticipant} from '@/entities/trip/models/TripParticipant.ts';

export class TripParticipantService {
    static async getAll() {
        return await axiosInstance.get<TripParticipant[]>(apiMap.GET_TRIP_PARTICIPANT);
    }

    static async getById(id:string) {
        const url = `${apiMap.GET_TRIP_PARTICIPANT}/${id}`;
        return await axiosInstance.get<TripParticipant>(url);
    }

    static async create(trip:TripParticipant) {
        return await axiosInstance.post(apiMap.GET_TRIP_PARTICIPANT, trip);
    }


    static async update(trip: TripParticipant) {
        const url = `${apiMap.GET_TRIP_PARTICIPANT}/${trip.id}`;
        return await axiosInstance.put(url, trip);
    }

    static async delete(id:string) {
        const url = `${apiMap.GET_TRIP_PARTICIPANT}/${id}`;
        return await axiosInstance.delete(url);
    }
}