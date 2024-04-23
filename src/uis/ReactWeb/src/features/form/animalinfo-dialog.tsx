import React from 'react';
import {Dialog, DialogContent} from "@/shared/ui/dialog.tsx";
import {Animal} from "@/entities/animal/Animal.ts";

interface IProps
{
    animal: Animal,
    isOpen:boolean,
    setIsOpen:(flag:boolean)=>void
}

const AnimalInfoDialog = ({animal, isOpen, setIsOpen}:IProps) => {
    return (
        <Dialog open={isOpen} onOpenChange={()=>setIsOpen(false)}>
            <DialogContent>

            </DialogContent>
        </Dialog>
    );
};

export default AnimalInfoDialog;