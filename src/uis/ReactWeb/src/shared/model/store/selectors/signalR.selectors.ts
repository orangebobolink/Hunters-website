import { RootState } from '@/shared/model/store';

export const selectSignalR = (state: RootState) => state.connection;