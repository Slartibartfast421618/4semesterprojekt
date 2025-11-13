import { useMemo } from "react";
import type { HairdresserView } from "./useHairdressersSearch";

export default function useFilteredHairdressers(
    data: HairdresserView[] | null,
    chosenTreatment: string
): HairdresserView[] | null {
    return useMemo(() => {
        if (!data) return null;
        if (!chosenTreatment) return data;
        return data.filter(h => h.treatments?.includes(chosenTreatment));
    }, [data, chosenTreatment]);
}