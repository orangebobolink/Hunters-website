import AnimalForm from "@/widgets/forms/ui/animal-form";
import {useEffect, useState} from "react";
import {useTranslation} from "react-i18next";
import {Animal} from "@/entities/animal/Animal.ts";
import {AnimalService} from "@/entities/animal/AnimalService.ts";
import translate from "google-translate-api-x";

const AnimalPage = () => {
    const [animals, setAnimals] = useState<Animal[]>([])
    const [isOpen, setIsOpen] = useState(false);
    const { t} = useTranslation("translation",
        {
            keyPrefix: "animal"
        });

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await AnimalService.getAll();

                setAnimals(response.data);
                console.log(response.data)
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchUsers();
    }, []);

    return (
        <div className="select-none h-full w-full flex items-center flex-col justify-center">
            <AnimalForm animals={animals} handleClick={(animal:Animal) => {}}/>
        </div>
    );
};

export default AnimalPage;