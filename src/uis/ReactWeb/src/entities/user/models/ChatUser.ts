import { Message } from "@/entities/chat/entities/Message";

export interface ChatUser {
    id?: string;
    avatarUrl: string;
    messages: Message[];
    firstName: string;
    lastName: string;
    groupId?: string;
}