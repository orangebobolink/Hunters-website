import { RootState } from '@/shared/model/store';

export const selectAuth = (state: RootState) => state.auth;
export const selectIsAuth = (state: RootState) => state.auth.isAuth;