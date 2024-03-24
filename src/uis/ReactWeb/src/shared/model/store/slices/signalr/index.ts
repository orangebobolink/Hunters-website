import {createSlice} from '@reduxjs/toolkit';
import {connectToSignalR} from '@/shared/model/store/slices/signalr/connectionThunk.ts';

const signalRSlice = createSlice({
    name: 'signalr',
    initialState: {
        connection: null,
        isConnected: false,
        error: null,
    },
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