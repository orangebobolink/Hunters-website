import { NavigationMenuItem, navigationMenuTriggerStyle } from '@/shared/ui';
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
} from '@/shared/ui/dropdown-menu.tsx';
import { Avatar, AvatarFallback, AvatarImage } from '@/shared/ui/avatar.tsx';
import { useTranslation } from 'react-i18next';
import { useActions } from '@/shared/lib/hooks/useActions.ts';
import { useAppSelector } from '@/shared/lib/hooks/redux-hooks.ts';
import { selectAuth } from '@/shared/model/store/selectors/auth.selectors.ts';
import { useNavigate } from 'react-router-dom';

const ProfileAvatarNavigate = () => {
    const { logout } = useActions();
    const { roles, avatarUrl } = useAppSelector(selectAuth);
    const navigate = useNavigate();

    const { t } = useTranslation('translation', {
        keyPrefix: 'header.profile',
    });

    const myTrips = () => {
        navigate('/profile/myTrips');
    };

    const setting = () => {
        navigate('/profile/settings');
    };

    const info = () => {
        navigate('/profile/inform');
    };

    return (
        <NavigationMenuItem className={navigationMenuTriggerStyle()}>
            <DropdownMenu>
                <DropdownMenuTrigger asChild>
                    <Avatar className='size-[2rem]'>
                        <AvatarImage src={avatarUrl} />
                        <AvatarFallback>CN</AvatarFallback>
                    </Avatar>
                </DropdownMenuTrigger>
                <DropdownMenuContent className='w-9 flex flex-col mr-9'>
                    <DropdownMenuLabel className='select-none'>
                        {t('tittle')}
                    </DropdownMenuLabel>
                    <DropdownMenuSeparator />
                    <DropdownMenuItem onClick={info}>
                        {t('information')}
                    </DropdownMenuItem>
                    {roles.includes('User') && (
                        <DropdownMenuItem onClick={myTrips}>
                            {t('myTrips')}
                        </DropdownMenuItem>
                    )}
                    <DropdownMenuItem onClick={setting}>
                        {t('settings')}
                    </DropdownMenuItem>
                    <DropdownMenuItem onClick={logout}>
                        {t('signOut')}
                    </DropdownMenuItem>
                </DropdownMenuContent>
            </DropdownMenu>
        </NavigationMenuItem>
    );
};

export default ProfileAvatarNavigate;
