import { apiMap } from '@/shared/const';
import { axiosInstance } from '@/shared/api/axiosInstance.ts';
import { Period } from '@/entities/report/models/Period.ts';
import { AnimalPopular } from '../models/AnimalPopular';

export class ReportService {
    static async getProductsByPopular(period: Period) {
        return await axiosInstance.get<ProductPopular[]>(
            apiMap.GET_REPORT_PRODUCT_POPULAR + `/${period}`
        );
    }

    static async getAnimalsByPopular(period: Period) {
        return await axiosInstance.get<AnimalPopular[]>(
            apiMap.GET_ANIMALS_BY_POPULAR + `/${period}`
        );
    }

    static async getProductsRevenueByPeriod(period: Period) {
        return await axiosInstance.get<ProductPeriodRevenue[]>(
            apiMap.GET_REPORT_PRODUCT_REVENUE + `/${period}`
        );
    }
}
