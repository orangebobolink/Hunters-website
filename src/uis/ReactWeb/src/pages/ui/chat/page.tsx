import Cookies from 'cookies-ts';
import {ChatLayout} from '@/widgets/chat/chat-layout.tsx';

export const ChatPage = () => {
    const cookies = new Cookies()
    const layout = cookies.get("react-resizable-panels:layout");
    const defaultLayout = layout
                          ? JSON.parse(layout.valueOf())
                          : undefined;

    return (
        <div className="w-full">
            <div className="z-10 border rounded-lg text-sm lg:flex">
                <ChatLayout defaultLayout={defaultLayout}
                            navCollapsedSize={8}/>
            </div>
        </div>
    );
}