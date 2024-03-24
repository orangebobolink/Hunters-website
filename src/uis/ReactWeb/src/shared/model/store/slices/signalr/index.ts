import {createSlice} from '@reduxjs/toolkit';
import {connectToSignalR} from '@/shared/model/store/slices/signalr/connectionThunk.ts';
import * as signalR from "@microsoft/signalr";

interface SignalRConnection {
    connection: signalR.HubConnection | null;
    isConnected: boolean;
    error: string | null;
}

export const signalRSlice = createSlice({
    name: 'signalr',
    initialState: {
        connection: null,
        isConnected: false,
        error: null,
    } as SignalRConnection,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(connectToSignalR.fulfilled, (state, action) => {
                state.connection = action.payload;
                state.isConnected = true;
            })
            .addCase(connectToSignalR.rejected, (state, action) => {
                state.error = action.error.message;
            });
    },
});

export const signalRMiddleware = (store) => (next) => async (action) => {
    if (action.type === 'signalr/connect') {
        if (!store.getState().signalr.isConnected) {
            await store.dispatch(connectToSignalR());
        }
    }
    return next(action);
};