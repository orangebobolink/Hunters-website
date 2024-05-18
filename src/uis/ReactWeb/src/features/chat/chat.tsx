import { useEffect, useState } from "react";
import ChatTopbar from '@/entities/chat/ui/chat-topbar.tsx';
import {ChatList} from '@/entities/chat/ui/chat-list.tsx';
import { ChatUser } from "@/entities/user/models/ChatUser";
import { Message } from "@/entities/chat/entities/Message";
import { SignalRContext } from "@/shared/model/signalrR";

interface ChatProps {
    selectedUser: ChatUser;
    isMobile: boolean;
}

export function Chat({ selectedUser, isMobile }: ChatProps) {
    const [messagesState, setMessages] = useState<Message[]>([]);
 
    useEffect(() => {
        if (selectedUser) {
            setMessages(selectedUser.messages);
        }
    }, [selectedUser]);
 
    const sendMessage = async (newMessage: Message) => {
        setMessages([...messagesState, newMessage]);

        newMessage.groupId = selectedUser.groupId!
        newMessage.toUserId = selectedUser.id!

        const response:boolean = await SignalRContext.invoke("CreateMessage", newMessage)!;
    };

    return (
        <div className="flex flex-col justify-between w-full h-[89dvh]">
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