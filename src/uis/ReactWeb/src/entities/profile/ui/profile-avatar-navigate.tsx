import {NavigationMenuItem, navigationMenuTriggerStyle} from '@/shared/ui';
import {
    DropdownMenu,
    DropdownMenuContent, DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger
} from '@/shared/ui/dropdown-menu.tsx';
import {Avatar, AvatarFallback, AvatarImage} from '@/shared/ui/avatar.tsx';
import {useTranslation} from 'react-i18next';

const ProfileAvatarNavigate = () => {
    const { t, i18n } = useTranslation("translation",
        {
            keyPrefix: "header.profile"
        });

    return (
        <NavigationMenuItem className={navigationMenuTriggerStyle()}>
            <DropdownMenu>
                <DropdownMenuTrigger asChild>
                    <Avatar className="size-[2rem]">
                        <AvatarImage src="https://github.com/shadcn.png" />
                        <AvatarFallback>CN</AvatarFallback>
                    </Avatar>
                </DropdownMenuTrigger>
                <DropdownMenuContent className="w-9 flex flex-col mr-9">
                    <DropdownMenuLabel className="select-none">
                        {t("tittle")}
                    </DropdownMenuLabel>
                    <DropdownMenuSeparator />
                    <DropdownMenuItem>
                        {t("information")}
                    </DropdownMenuItem>
                    <DropdownMenuItem>
                        {t("settings")}
                    </DropdownMenuItem>
                    <DropdownMenuItem>
                        {t("signOut")}
                    </DropdownMenuItem>
                </DropdownMenuContent>
            </DropdownMenu>
        </NavigationMenuItem>
    );
};

export default ProfileAvatarNavigate;