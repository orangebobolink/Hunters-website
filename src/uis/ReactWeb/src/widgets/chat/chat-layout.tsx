"use client";

import { userData } from "./data";
import React, { useEffect, useState } from "react";
import { cn } from "@/shared/lib/utils/cnUtil.ts";
import {ResizableHandle, ResizablePanel, ResizablePanelGroup} from '@/shared/ui/resizable.tsx';
import {Sidebar} from '@/features/chat/sidebar.tsx';
import {Chat} from '@/features/chat/chat.tsx';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {Button} from "@/shared/ui";
import {SignalRContext} from '@/shared/model/signalrR';

interface ChatLayoutProps {
    defaultLayout: number[] | undefined;
    defaultCollapsed?: boolean;
    navCollapsedSize: number;
}

export function ChatLayout({
                               defaultLayout = [320, 480],
                               defaultCollapsed = false,
                               navCollapsedSize,
                           }: ChatLayoutProps) {
    const {id, error} = useAppSelector(selectAuth);
    const [isCollapsed, setIsCollapsed] = React.useState(defaultCollapsed);
    const [selectedUser, setSelectedUser] = React.useState(userData[0]);
    const [isMobile, setIsMobile] = useState(false);

    useEffect(() => {
        const fetchData = async () => {
            const response = await SignalRContext.invoke("ReceiveMessages", id);
            console.log("ЧТО-ТО " + JSON.stringify(response));
        };

        fetchData().catch(console.error);
    }, []);


    useEffect(() => {
        console.log(id)
        const checkScreenWidth = () => {
            setIsMobile(window.innerWidth <= 768);
        };

        // Initial check
        checkScreenWidth();

        // Event listener for screen width changes
        window.addEventListener("resize", checkScreenWidth);

        // Cleanup the event listener on component unmount
        return () => {
            window.removeEventListener("resize", checkScreenWidth);
        };
    }, []);

    return (
        <ResizablePanelGroup
            direction="horizontal"
            onLayout={(sizes: number[]) => {
                document.cookie = `react-resizable-panels:layout=${JSON.stringify(
                    sizes
                )}`;
            }}
            className="h-full items-stretch"
        >
            <ResizablePanel
                defaultSize={defaultLayout[0]}
                collapsedSize={navCollapsedSize}
                collapsible={true}
                minSize={isMobile ? 0 : 24}
                maxSize={isMobile ? 8 : 30}
                onCollapse={() => {
                    setIsCollapsed(true);
                    document.cookie = `react-resizable-panels:collapsed=${JSON.stringify(
                        true
                    )}`;
                }}
                onExpand={() => {
                    setIsCollapsed(false);
                    document.cookie = `react-resizable-panels:collapsed=${JSON.stringify(
                        false
                    )}`;
                }}
                className={cn(
                    isCollapsed && "min-w-[50px] md:min-w-[70px] transition-all duration-300 ease-in-out"
                )}
            >
                <Sidebar
                    isCollapsed={isCollapsed || isMobile}
                    links={userData.map((user) => ({
                        name: user.name,
                        messages: user.messages ?? [],
                        avatar: user.avatar,
                        variant: selectedUser.name === user.name ? "grey" : "ghost",
                    }))}
                    isMobile={isMobile}
                />
            </ResizablePanel>
            <ResizableHandle withHandle />
            <ResizablePanel defaultSize={defaultLayout[1]} minSize={30}>
                <Chat
                    messages={selectedUser.messages}
                    selectedUser={selectedUser}
                    isMobile={isMobile}
                />
            </ResizablePanel>
        </ResizablePanelGroup>
    );
}