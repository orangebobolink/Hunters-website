import {
    FileImage,
    Mic,
    Paperclip,
    PlusCircle,
    SendHorizontal,
    ThumbsUp,
} from "lucide-react";
import React, { useRef, useState } from "react";
import { AnimatePresence, motion } from "framer-motion";
import { Message, loggedInUserData } from "@/widgets/chat/data.tsx";
import { EmojiPicker } from "@/features/chat/emoji-picker.tsx";
import {Link} from 'react-router-dom';
import {cn} from '@/shared/lib';
import {buttonVariants} from '@/shared/ui';
import {Popover, PopoverContent, PopoverTrigger} from '@/shared/ui/popover.tsx';
import {Textarea} from '@/shared/ui/textarea.tsx';

interface ChatBottombarProps {
    sendMessage: (newMessage: Message) => void;
    isMobile: boolean;
}

export const BottombarIcons = [{ icon: FileImage }, { icon: Paperclip }];

export default function ChatBottombar({
                                          sendMessage, isMobile,
                                      }: ChatBottombarProps) {
    const [message, setMessage] = useState("");
    const inputRef = useRef<HTMLTextAreaElement>(null);

    const handleInputChange = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
        setMessage(event.target.value);
    };

    const handleThumbsUp = () => {
        const newMessage: Message = {
            id: message.length + 1,
            name: loggedInUserData.name,
            avatar: loggedInUserData.avatar,
            message: "👍",
        };
        sendMessage(newMessage);
        setMessage("");
    };

    const handleSend = () => {
        if (message.trim()) {
            const newMessage: Message = {
                id: message.length + 1,
                name: loggedInUserData.name,
                avatar: loggedInUserData.avatar,
                message: message.trim(),
            };
            sendMessage(newMessage);
            setMessage("");

            if (inputRef.current) {
                inputRef.current.focus();
            }
        }
    };

    const handleKeyPress = (event: React.KeyboardEvent<HTMLTextAreaElement>) => {
        if (event.key === "Enter" && !event.shiftKey) {
            event.preventDefault();
            handleSend();
        }

        if (event.key === "Enter" && event.shiftKey) {
            event.preventDefault();
            setMessage((prev) => prev + "\n");
        }
    };

    return (
        <div className="p-2 flex justify-between w-full items-center gap-2">
            <div className="flex">
                <Popover>
                    <PopoverTrigger asChild>
                        <Link
                            to="#"
                            className={cn(
                                buttonVariants({ variant: "ghost", size: "icon" }),
                                "h-9 w-9",
                                "dark:bg-muted dark:text-muted-foreground dark:hover:bg-muted dark:hover:text-white"
                            )}
                        >
                            <PlusCircle size={20} className="text-muted-foreground" />
                        </Link>
                    </PopoverTrigger>
                    <PopoverContent
                        side="top"
                        className="w-full p-2">
                        {message.trim() || isMobile ? (
                            <div className="flex gap-2">
                                <Link
                                    to="#"
                                    className={cn(
                                        buttonVariants({ variant: "ghost", size: "icon" }),
                                        "h-9 w-9",
                                        "dark:bg-muted dark:text-muted-foreground dark:hover:bg-muted dark:hover:text-white"
                                    )}
                                >
                                    <Mic size={20} className="text-muted-foreground" />
                                </Link>
                                {BottombarIcons.map((icon, index) => (
                                    <Link
                                        key={index}
                                        to="#"
                                        className={cn(
                                            buttonVariants({ variant: "ghost", size: "icon" }),
                                            "h-9 w-9",
                                            "dark:bg-muted dark:text-muted-foreground dark:hover:bg-muted dark:hover:text-white"
                                        )}
                                    >
                                        <icon.icon size={20} className="text-muted-foreground" />
                                    </Link>
                                ))}
                            </div>
                        ) : (
                             <Link
                                 to="#"
                                 className={cn(
                                     buttonVariants({ variant: "ghost", size: "icon" }),
                                     "h-9 w-9",
                                     "dark:bg-muted dark:text-muted-foreground dark:hover:bg-muted dark:hover:text-white"
                                 )}
                             >
                                 <Mic size={20} className="text-muted-foreground" />
                             </Link>
                         )}
                    </PopoverContent>
                </Popover>
                {!message.trim() && !isMobile && (
                    <div className="flex">
                        {BottombarIcons.map((icon, index) => (
                            <Link
                                key={index}
                                to="#"
                                className={cn(
                                    buttonVariants({ variant: "ghost", size: "icon" }),
                                    "h-9 w-9",
                                    "dark:bg-muted dark:text-muted-foreground dark:hover:bg-muted dark:hover:text-white"
                                )}
                            >
                                <icon.icon size={20} className="text-muted-foreground" />
                            </Link>
                        ))}
                    </div>
                )}
            </div>

            <AnimatePresence initial={false}>
                <motion.div
                    key="input"
                    className="w-full relative"
                    layout
                    initial={{ opacity: 0, scale: 1 }}
                    animate={{ opacity: 1, scale: 1 }}
                    exit={{ opacity: 0, scale: 1 }}
                    transition={{
                        opacity: { duration: 0.05 },
                        layout: {
                            type: "spring",
                            bounce: 0.15,
                        },
                    }}
                >
                    <Textarea
                        autoComplete="off"
                        value={message}
                        ref={inputRef}
                        onKeyDown={handleKeyPress}
                        onChange={handleInputChange}
                        name="message"
                        placeholder="Aa"
                        className=" w-full border rounded-full flex items-center h-9 resize-none overflow-hidden bg-background"
                    />
                    <div className="absolute right-2 bottom-[15px]  ">
                        <EmojiPicker onChange={(value) => {
                            setMessage(message + value)
                            if (inputRef.current) {
                                inputRef.current.focus();
                            }
                        }} />
                    </div>
                </motion.div>

                {message.trim() ? (
                    <Link
                        to="#"
                        className={cn(
                            buttonVariants({ variant: "ghost", size: "icon" }),
                            "h-9 w-9",
                            "dark:bg-muted dark:text-muted-foreground dark:hover:bg-muted dark:hover:text-white shrink-0"
                        )}
                        onClick={handleSend}
                    >
                        <SendHorizontal size={20} className="text-muted-foreground" />
                    </Link>
                ) : (
                     <Link
                         to="#"
                         className={cn(
                             buttonVariants({ variant: "ghost", size: "icon" }),
                             "h-9 w-9",
                             "dark:bg-muted dark:text-muted-foreground dark:hover:bg-muted dark:hover:text-white shrink-0"
                         )}
                         onClick={handleThumbsUp}
                     >
                         <ThumbsUp size={20} className="text-muted-foreground" />
                     </Link>
                 )}
            </AnimatePresence>
        </div>
    );
}