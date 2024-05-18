import {apiMap} from '@/shared/const';
import {axiosInstance} from '@/shared/api/axiosInstance.ts';
import {Animal} from '@/entities/animal/models/Animal.ts';

export class AnimalService {
    static async getAll() {
        return await axiosInstance.get<Animal[]>(apiMap.GET_ANIMALS);
    }

    static async create(animal:Animal) {
        return await axiosInstance.post(apiMap.GET_ANIMALS, animal);
    }

    static async update(animal: Animal) {
        const url = `${apiMap.GET_ANIMALS}/${animal.id}`;
        return await axiosInstance.put(url, animal);
    }
}