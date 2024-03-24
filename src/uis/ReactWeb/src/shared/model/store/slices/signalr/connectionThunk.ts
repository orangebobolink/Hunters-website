import {createAsyncThunk} from '@reduxjs/toolkit';
import * as signalR from "@microsoft/signalr";

export const connectToSignalR = createAsyncThunk(
    'signalr/connect',
    async (_, { getState }) => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5001/chat")
            .build();

        connection.on("ReceiveMessage", (message) => {
            console.log(message)
        });

        try {
            await connection.start();
        } catch (err) {
            console.error(err);
        }

        // Возвращаем соединение, чтобы его можно было использовать в других местах приложения
        return connection;
    }
);