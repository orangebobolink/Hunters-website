import {LocalStorageItems} from '@/shared/const';

export class LocaleStorageUtils {
    static setAccessToken = (value: string) => localStorage.setItem(LocalStorageItems.accessToken, value);

    static getAccessToken = () => localStorage.getItem(LocalStorageItems.accessToken);

    static removeAccessToken = () => localStorage.removeItem(LocalStorageItems.accessToken);
}