import {BackendEndpoints} from '@/shared/const/BackendEndpoints.ts';


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
};