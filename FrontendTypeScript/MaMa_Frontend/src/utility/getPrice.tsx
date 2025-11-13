import type { HairdresserView } from "../hooks/useHairdressersSearch";

export default function getPrice(
    fData: HairdresserView,
    cTreatment: string | null
): number | null {

    if (!fData.treatments) return null;

    // No selected treatment = Take lowest price
    if (!cTreatment) {
        const allPrices = fData.treatments
            .map(t => t.treatmentPrice)
            .filter((p): p is number => p !== null);

        return allPrices.length ? Math.min(...allPrices) : null;
    }

    // Find price for specific treatment
    const match = fData.treatments?.find(
        t => t.treatmentType === cTreatment && t.treatmentPrice !== null
    );

    return match ? match.treatmentPrice : null;
}