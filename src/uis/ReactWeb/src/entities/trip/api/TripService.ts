import {apiMap} from '@/shared/const';
import {axiosInstance} from '@/shared/api/axiosInstance.ts';
import {Trip} from '@/entities/trip/models/Trip.ts';

export class TripService {
    static async getAll() {
        return await axiosInstance.get<Trip[]>(apiMap.GET_TRIP+ "/include");
    }

    static async getByParticipantId(id:string) {
        const url = `${apiMap.GET_TRIP}/user/${id}`;
        return await axiosInstance.get<Trip[]>(url);
    }

    static async create(trip:Trip) {
        return await axiosInstance.post(apiMap.GET_TRIP, trip);
    }

    static async payTrip(number: string) {
        const url = `${apiMap.PAYMENT}/trip/${number}`;
        return await axiosInstance.post<boolean>(url);
    }


    static async update(trip: Trip) {
        const url = `${apiMap.GET_TRIP}/${trip.id}`;
        return await axiosInstance.put(url, trip);
    }

    static async delete(id:string) {
        const url = `${apiMap.GET_TRIP}/${id}`;
        return await axiosInstance.delete(url);
    }
}