import {NavigationMenuItem, NavigationMenuLink, navigationMenuTriggerStyle} from '@/shared/ui';
import {EnvelopeClosedIcon, EnvelopeOpenIcon} from '@radix-ui/react-icons';
import {NavLink} from 'react-router-dom';

const ChatNavItem = () => {
    return (
        <NavigationMenuItem className={navigationMenuTriggerStyle()}>
            <NavLink
                to="/chat"
            >
                {({ isActive}) => (
                    <NavigationMenuLink>
                        {
                            isActive
                            ?  <EnvelopeOpenIcon className="px-2 size-[2.5rem]"/>
                            :  <EnvelopeClosedIcon className="px-2 size-[2.5rem]"/>
                        }
                    </NavigationMenuLink>
                )}
            </NavLink>
        </NavigationMenuItem>
    );
};

export default ChatNavItem;