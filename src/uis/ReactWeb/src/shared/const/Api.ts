import { BackendEndpoints } from '@/shared/const/BackendEndpoints.ts';

const BASE_URL = 'http://localhost:5001';
const BASE_API_URL = 'http://localhost:5001/api';

export const apiMap = {
    BASE_URL,
    BASE_API_URL,
    GET_USER: `${BASE_URL}/${BackendEndpoints.GET_USER}`,
    GET_USERS: `${BASE_API_URL}/${BackendEndpoints.GET_USERS}`,
    CREATE_USER: `${BASE_API_URL}/${BackendEndpoints.GET_USERS}`,
    UPDATE_USER: `${BASE_API_URL}/${BackendEndpoints.GET_USERS}`,
    LOGIN: `${BASE_API_URL}/${BackendEndpoints.LOGIN}`,
    REFRESH: `${BASE_API_URL}/${BackendEndpoints.REFRESH}`,
    LOGOUT: `${BASE_API_URL}/${BackendEndpoints.LOGOUT}`,
    GET_ANIMALS: `${BASE_API_URL}/${BackendEndpoints.GET_ANIMALS}`,
    GET_HUNTINGSEASSON: `${BASE_API_URL}/${BackendEndpoints.GET_HUNTINGSEASSON}`,
    GET_FEEDING: `${BASE_API_URL}/${BackendEndpoints.GET_FEEDING}`,
    GET_RANGERS: `${BASE_API_URL}/${BackendEndpoints.GET_RANGERS}`,
    GET_LANDS: `${BASE_API_URL}/${BackendEndpoints.GET_LANDS}`,
    GET_PRODUCTS: `${BASE_API_URL}/${BackendEndpoints.GET_PRODUCTS}`,
    GET_RAIDS: `${BASE_API_URL}/${BackendEndpoints.GET_RAIDS}`,
    GET_PERMISSION: `${BASE_API_URL}/${BackendEndpoints.GET_PERMISSION}`,
    GET_TRIP: `${BASE_API_URL}/${BackendEndpoints.GET_TRIP}`,
    GET_HUNTINGLICENSE: `${BASE_API_URL}/${BackendEndpoints.GET_HUNTINGLICENSE}`,
    PAYMENT: `${BASE_API_URL}/${BackendEndpoints.PAYMENT}`,
    GET_TRIP_PARTICIPANT: `${BASE_API_URL}/${BackendEndpoints.GET_TRIP_PARTICIPANT}`,
    GET_PRODUCT: `${BASE_API_URL}/${BackendEndpoints.GET_PRODUCT}`,
    GET_RENT_PRODUCT: `${BASE_API_URL}/${BackendEndpoints.GET_RENT_PRODUCT}`,
    GET_REPORT_PRODUCT_POPULAR: `${BASE_API_URL}/${BackendEndpoints.GET_REPORT_PRODUCT_POPULAR}`,
    GET_REPORT_PRODUCT_REVENUE: `${BASE_API_URL}/${BackendEndpoints.GET_REPORT_PRODUCT_REVENUE}`,
    GET_ANIMALS_BY_POPULAR: `${BASE_API_URL}/${BackendEndpoints.GET_ANIMALS_BY_POPULAR}`,
};
