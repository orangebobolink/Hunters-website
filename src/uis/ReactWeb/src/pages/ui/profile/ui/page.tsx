import {
    NavigationMenu,
    NavigationMenuItem,
    NavigationMenuLink,
    NavigationMenuList,
    navigationMenuTriggerStyle,
} from '@/shared/ui';
import { ScrollArea } from '@/shared/ui/scroll-area';
import { Link, Outlet } from 'react-router-dom';

export default function ProfilePage() {
    return (
        <div className='flex w-full flex-row justify-center'>
            <div className='w-1/5 mx-5'>
                <NavigationMenu orientation='vertical'>
                    <NavigationMenuList className='flex-col item-start'>
                        <NavigationMenuItem>
                            <Link to='/profile/inform'>
                                <NavigationMenuLink
                                    className={navigationMenuTriggerStyle()}
                                >
                                    Информация
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                        <NavigationMenuItem>
                            <Link to='/profile/myTrips'>
                                <NavigationMenuLink
                                    className={navigationMenuTriggerStyle()}
                                >
                                    Мои путевки
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                        <NavigationMenuItem>
                            <Link to='/profile/settings'>
                                <NavigationMenuLink
                                    className={navigationMenuTriggerStyle()}
                                >
                                    Настройки
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                    </NavigationMenuList>
                </NavigationMenu>
            </div>
            <div className='flex gap-4 mx-5'>
                <ScrollArea className='h-[85dvh] w-[75dvw] rounded-md border'>
                    <Outlet />
                </ScrollArea>
            </div>
        </div>
    );
}

function BellIcon(props) {
    return (
        <svg
            {...props}
            xmlns='http://www.w3.org/2000/svg'
            width='24'
            height='24'
            viewBox='0 0 24 24'
            fill='none'
            stroke='currentColor'
            strokeWidth='2'
            strokeLinecap='round'
            strokeLinejoin='round'
        >
            <path d='M6 8a6 6 0 0 1 12 0c0 7 3 9 3 9H3s3-2 3-9' />
            <path d='M10.3 21a1.94 1.94 0 0 0 3.4 0' />
        </svg>
    );
}

function LockIcon(props) {
    return (
        <svg
            {...props}
            xmlns='http://www.w3.org/2000/svg'
            width='24'
            height='24'
            viewBox='0 0 24 24'
            fill='none'
            stroke='currentColor'
            strokeWidth='2'
            strokeLinecap='round'
            strokeLinejoin='round'
        >
            <rect width='18' height='11' x='3' y='11' rx='2' ry='2' />
            <path d='M7 11V7a5 5 0 0 1 10 0v4' />
        </svg>
    );
}

function UserIcon(props) {
    return (
        <svg
            {...props}
            xmlns='http://www.w3.org/2000/svg'
            width='24'
            height='24'
            viewBox='0 0 24 24'
            fill='none'
            stroke='currentColor'
            strokeWidth='2'
            strokeLinecap='round'
            strokeLinejoin='round'
        >
            <path d='M19 21v-2a4 4 0 0 0-4-4H9a4 4 0 0 0-4 4v2' />
            <circle cx='12' cy='7' r='4' />
        </svg>
    );
}
