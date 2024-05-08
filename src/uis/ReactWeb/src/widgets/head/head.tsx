import React from 'react';
import {
    NavigationMenu,
    NavigationMenuContent,
    NavigationMenuItem,
    NavigationMenuLink,
    NavigationMenuList,
    navigationMenuLogoTriggerStyle,
    NavigationMenuTrigger,
    navigationMenuTriggerStyle
} from '@/shared/ui/navigation-menu.tsx';
import {cn} from '@/shared/lib';
import {Link} from 'react-router-dom';
import logo from '@/assets/logo.png';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import ProfileAvatarNavigate from '@/entities/profile/ui/profile-avatar-navigate.tsx';
import AuthNavButtons from '@/entities/(auth)/ui/auth-nav-buttons.tsx';
import LanguageNavItem from '@/shared/ui/custom/language-nav-item.tsx';
import ThemeNavItem from '@/shared/ui/custom/theme-nav-item.tsx';
import ChatNavItem from '@/entities/chat/ui/chat-nav-item.tsx';
import {useTranslation} from 'react-i18next';

const components: { title: string; href: string; description: string }[] = [
    {
        title: "rent.ammunition.tittle",
        href: "/docs/primitives/alert-dialog",
        description: "rent.ammunition.description",
    },
    {
        title: "rent.gun.tittle",
        href: "/docs/primitives/hover-card",
        description: "rent.gun.description",
    },
    {
        title: "rent.car.tittle",
        href: "/docs/primitives/progress",
        description:"rent.car.description",
    },

]

export function Head() {
    const { isAuth, roles } = useAppSelector(selectAuth);
    const { t} = useTranslation("translation",
        {
            keyPrefix: "header.nav"
        });

    return (
        <NavigationMenu className="w-full px-10 justify-between items-center flex m-auto">
            <NavigationMenuList className="flex flex-row justify-between items-center w-full">
                <NavigationMenuItem className="flex flex-row items-center">
                    <img src={logo} className="size-[3rem]" alt="logo"/>
                    <Link to="/" >
                        <NavigationMenuLink className={navigationMenuLogoTriggerStyle()}>
                            {t('webname')}
                        </NavigationMenuLink>
                    </Link>
                </NavigationMenuItem>
                {(roles.includes("User") || roles.length == 0)
                    &&
                    <>
                        <NavigationMenuItem>
                            <Link to="/trip" >
                                <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                    {t("trip")}
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                        <NavigationMenuItem>
                            <Link to="/paymantFee" >
                                <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                    {t("paymantFee")}
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                        <NavigationMenuItem>
                            <Link to="/checkHuntingLicense" >
                                <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                    {t("checkHuntingLicense")}
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                        <NavigationMenuItem>
                            <NavigationMenuTrigger>
                                {t("rent.tittle")}
                            </NavigationMenuTrigger>
                            <NavigationMenuContent>
                                <ul className="grid w-[400px] gap-3 p-4 md:w-[500px] md:grid-cols-1 lg:w-[600px] ">
                                    {components.map((component) => (
                                        <ListItem
                                            key={component.title}
                                            title={t(component.title)}
                                            href={component.href}
                                        >
                                            {t(component.description)}
                                        </ListItem>
                                    ))}
                                </ul>
                            </NavigationMenuContent>
                        </NavigationMenuItem>
                    </>
                }
                {roles.includes("Admin") &&
                     <NavigationMenuItem>
                         <Link to="/managing" >
                             <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                 {t("managing")}
                             </NavigationMenuLink>
                         </Link>
                     </NavigationMenuItem>
                }
                {roles.includes("Manager") &&
                    <>
                        <NavigationMenuItem>
                            <Link to="/trip" >
                                <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                    {t("trip")}
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                        <NavigationMenuItem>
                            <Link to="/permission" >
                                <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                    {t("permission")}
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                        <NavigationMenuItem>
                            <Link to="/feeding" >
                                <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                    {t("feeding")}
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                        <NavigationMenuItem>
                            <Link to="/animal" >
                                <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                    {t("animal")}
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                        <NavigationMenuItem>
                            <Link to="/reporting" >
                                <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                    {t("reporting")}
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                    </>
                }
                {roles.includes("Ranger") &&
                    <>
                        <NavigationMenuItem>
                            <Link to="/trip" >
                                <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                    {t("trip")}
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                        <NavigationMenuItem>
                            <Link to="/raid" >
                                <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                    {t("raid")}
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                        <NavigationMenuItem>
                            <Link to="/feeding" >
                                <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                    {t("feeding")}
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                    </>
                }
                {roles.includes("Director") &&
                    <>
                        <NavigationMenuItem>
                            <Link to="/reporting" >
                                <NavigationMenuLink className={navigationMenuTriggerStyle()}>
                                    {t("reporting")}
                                </NavigationMenuLink>
                            </Link>
                        </NavigationMenuItem>
                    </>
                }
            </NavigationMenuList>
            <NavigationMenuList className="w-full">
                {
                    isAuth
                    ? <ChatNavItem/>
                    : <div></div>
                }
                <LanguageNavItem/>
                <ThemeNavItem/>
                {
                    isAuth
                    ? <ProfileAvatarNavigate/>
                    : <AuthNavButtons/>
                }
            </NavigationMenuList>
        </NavigationMenu>
    )
}

const ListItem = React.forwardRef<
    React.ElementRef<"a">,
    React.ComponentPropsWithoutRef<"a">
>(({ className, title, children, ...props }, ref) => {
    return (
        <li>
            <NavigationMenuLink asChild>
                <a
                    ref={ref}
                    className={cn(
                        "block select-none space-y-1 rounded-md p-3 leading-none no-underline outline-none transition-colors hover:bg-accent hover:text-accent-foreground focus:bg-accent focus:text-accent-foreground",
                        className
                    )}
                    {...props}
                >
                    <div className="text-sm font-medium leading-none">{title}</div>
                    <p className="line-clamp-2 text-sm leading-snug text-muted-foreground">
                        {children}
                    </p>
                </a>
            </NavigationMenuLink>
        </li>
    )
})
ListItem.displayName = "ListItem"
