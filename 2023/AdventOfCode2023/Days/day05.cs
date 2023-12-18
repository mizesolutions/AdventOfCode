namespace AdventOfCode2023.Days
{
    public class Day05 : BaseDay
    {
        public List<string> Seeds { get; set; }
        public List<(string, string, string)> SeedToSoilMap { get; set; }
        public List<(string, string, string)> SoilToFertilizerMap { get; set; }
        public List<(string, string, string)> FertilizerToWaterMap { get; set; }
        public List<(string, string, string)> WaterToLightMap { get; set; }
        public List<(string, string, string)> LightToTemperatureMap { get; set; }
        public List<(string, string, string)> TemperatureToHumidityMap { get; set; }
        public List<(string, string, string)> HumidityToLocationMap { get; set; }

        public Day05(string day, bool hasInput) : base(day, hasInput)
        {
            Seeds = new();
            SeedToSoilMap = new();
            SoilToFertilizerMap = new();
            FertilizerToWaterMap = new();
            WaterToLightMap = new();
            LightToTemperatureMap = new();
            TemperatureToHumidityMap = new();
            HumidityToLocationMap = new();
        }

        public override void PuzzleOne()
        {
            PrintCurrentMethod();

            CreateMaps();


            PrintResults(Result1);
        }

        public override void PuzzleTwo()
        {
            PrintCurrentMethod();
            PrintResults(Result2);
        }

        private void CreateMaps()
        {
            var s = FileInput;
            for (var i = 0; i <= FileInput.Count; i++)
            {
                if (s[i].Length == 0)
                {
                    continue;
                }
                else if (s[i].Contains("seeds:"))
                {
                    Seeds = s[i].Split().ToList();
                    Seeds.Remove("seeds:");
                }
                else if (s[i].Contains("seed-to-soil map:"))
                {
                    i++;
                    while (s[i].Length > 0)
                    {
                        var t = s[i].Split().ToList();
                        SeedToSoilMap.Add((t[0], t[1], t[2]));
                        i++;
                    }
                }
                else if (s[i].Contains("soil-to-fertilizer map:"))
                {
                    i++;
                    while (s[i].Length > 0)
                    {
                        var t = s[i].Split().ToList();
                        SoilToFertilizerMap.Add((t[0], t[1], t[2]));
                        i++;
                    }
                }
                else if (s[i].Contains("fertilizer-to-water map:"))
                {
                    i++;
                    while (s[i].Length > 0)
                    {
                        var t = s[i].Split().ToList();
                        FertilizerToWaterMap.Add((t[0], t[1], t[2]));
                        i++;
                    }
                }
                else if (s[i].Contains("water-to-light map:"))
                {
                    i++;
                    while (s[i].Length > 0)
                    {
                        var t = s[i].Split().ToList();
                        WaterToLightMap.Add((t[0], t[1], t[2]));
                        i++;
                    }
                }
                else if (s[i].Contains("light-to-temperature map:"))
                {
                    i++;
                    while (s[i].Length > 0)
                    {
                        var t = s[i].Split().ToList();
                        LightToTemperatureMap.Add((t[0], t[1], t[2]));
                        i++;
                    }
                }
                else if (s[i].Contains("temperature-to-humidity map:"))
                {
                    i++;
                    while (s[i].Length > 0)
                    {
                        var t = s[i].Split().ToList();
                        TemperatureToHumidityMap.Add((t[0], t[1], t[2]));
                        i++;
                    }
                }
                else if (s[i].Contains("humidity-to-location map:"))
                {
                    i++;
                    while (i < s.Count)
                    {
                        var t = s[i].Split().ToList();
                        HumidityToLocationMap.Add((t[0], t[1], t[2]));
                        i++;
                    }
                }
            }
        }
    }
}