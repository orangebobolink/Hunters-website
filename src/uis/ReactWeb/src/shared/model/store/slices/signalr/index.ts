import {createSlice} from '@reduxjs/toolkit';
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

    },
});