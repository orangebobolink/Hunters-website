import { Avatar, AvatarImage } from '@/shared/ui/avatar.tsx';
import { ChatUser } from '@/entities/user/models/ChatUser';

interface ChatTopbarProps {
    selectedUser: ChatUser;
}

export default function ChatTopbar({ selectedUser }: ChatTopbarProps) {
    return (
        <div className='w-full h-20 flex p-4 justify-between items-center border-b'>
            <div className='flex items-center gap-2'>
                <Avatar className='flex justify-center items-center'>
                    <AvatarImage
                        src={selectedUser?.avatarUrl}
                        alt={selectedUser?.avatarUrl}
                        width={6}
                        height={6}
                        className='w-10 h-10 '
                    />
                </Avatar>
                <div className='flex flex-col'>
                    <span className='font-medium'>
                        {selectedUser?.firstName + ' ' + selectedUser?.lastName}
                    </span>
                </div>
            </div>
        </div>
    );
}
