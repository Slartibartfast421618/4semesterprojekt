export default function TreatmentDropdown({ treatments, selected, onChange }: {
    treatments: string[];
    selected: string;
    onChange: (value: string) => void;
}) {
    return (
        <select
            value={selected}
            onChange={(e) => onChange(e.target.value)}
            className="dropdown"
        >
            <option value="">All treatments</option>
            {treatments.map((t, i) => (
                <option key={i} value={t}>
                    {t}
                </option>
            ))}
        </select>
    );
}