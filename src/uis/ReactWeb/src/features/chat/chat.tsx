import { Message, UserData } from "@/widgets/chat/data.tsx";
import React from "react";
import ChatTopbar from '@/entities/chat/ui/chat-topbar.tsx';
import {ChatList} from '@/entities/chat/ui/chat-list.tsx';

interface ChatProps {
    messages?: Message[];
    selectedUser: UserData;
    isMobile: boolean;
}

export function Chat({ messages, selectedUser, isMobile }: ChatProps) {
    const [messagesState, setMessages] = React.useState<Message[]>(
        messages ?? []
    );

    const sendMessage = (newMessage: Message) => {
        setMessages([...messagesState, newMessage]);
    };

    return (
        <div className="flex flex-col justify-between w-full h-[93dvh]">
            <ChatTopbar selectedUser={selectedUser} />

            <ChatList
                messages={messagesState}
                selectedUser={selectedUser}
                sendMessage={sendMessage}
                isMobile={isMobile}
            />
        </div>
    );
}