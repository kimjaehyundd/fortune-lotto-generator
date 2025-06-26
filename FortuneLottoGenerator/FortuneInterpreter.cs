using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FortuneLottoGenerator
{
    public class FortuneInterpretation
    {
        public string Explanation { get; set; }
        public Dictionary<string, int> ElementValues { get; set; }
        public List<int> LuckyNumbers { get; set; }
        public string MainElement { get; set; }

        public FortuneInterpretation()
        {
            ElementValues = new Dictionary<string, int>();
            LuckyNumbers = new List<int>();
        }
    }

    public class FortuneInterpreter
    {
        private readonly Dictionary<string, int> elementMappings;
        private readonly Dictionary<string, string> emotionElements;
        private readonly Dictionary<string, string> foodElements;
        private readonly Dictionary<string, string> timeElements;
        private readonly Dictionary<string, string> actionElements;

        public FortuneInterpreter()
        {
            InitializeMappings();
        }

        private void InitializeMappings()
        {
            // 오행 속성과 숫자 매핑
            elementMappings = new Dictionary<string, int>();
            elementMappings.Add("금", 1);
            elementMappings.Add("목", 3);
            elementMappings.Add("물", 6);
            elementMappings.Add("불", 2);
            elementMappings.Add("토", 5);

            // 감정과 오행 매핑
            emotionElements = new Dictionary<string, string>();
            emotionElements.Add("배고", "토");
            emotionElements.Add("배가고파", "토");
            emotionElements.Add("배가고픈", "토");
            emotionElements.Add("슬픈", "금");
            emotionElements.Add("슬펐", "금");
            emotionElements.Add("슬펐다", "금");
            emotionElements.Add("기쁜", "불");
            emotionElements.Add("행복", "불");
            emotionElements.Add("좋아", "불");
            emotionElements.Add("화나", "불");
            emotionElements.Add("화난", "불");
            emotionElements.Add("짜증", "불");
            emotionElements.Add("고민", "목");
            emotionElements.Add("걱정", "목");
            emotionElements.Add("스트레스", "목");
            emotionElements.Add("피곤", "물");
            emotionElements.Add("지쳐", "물");
            emotionElements.Add("느릿", "물");

            // 음식과 오행 매핑
            foodElements = new Dictionary<string, string>();
            foodElements.Add("라면", "물");
            foodElements.Add("국물", "물");
            foodElements.Add("물", "물");
            foodElements.Add("고기", "불");
            foodElements.Add("소시지", "불");
            foodElements.Add("돼지", "불");
            foodElements.Add("치킨", "불");
            foodElements.Add("야채", "목");
            foodElements.Add("샐러드", "목");
            foodElements.Add("상추", "목");
            foodElements.Add("빵", "토");
            foodElements.Add("밥", "토");
            foodElements.Add("감자", "토");
            foodElements.Add("우유", "금");
            foodElements.Add("달걀", "금");
            foodElements.Add("아이스크림", "금");

            // 시간과 오행 매핑
            timeElements = new Dictionary<string, string>();
            timeElements.Add("아침", "목");
            timeElements.Add("새벽", "목");
            timeElements.Add("오전", "목");
            timeElements.Add("점심", "불");
            timeElements.Add("낮", "불");
            timeElements.Add("오후", "불");
            timeElements.Add("저녁", "토");
            timeElements.Add("저녁식사", "토");
            timeElements.Add("저녁때", "토");
            timeElements.Add("밤", "물");
            timeElements.Add("야식", "물");
            timeElements.Add("늦은밤", "물");
            timeElements.Add("어제", "금");
            timeElements.Add("어제밤", "금");
            timeElements.Add("어젠", "금");

            // 행동과 오행 매핑
            actionElements = new Dictionary<string, string>();
            actionElements.Add("먹었", "토");
            actionElements.Add("먹어", "토");
            actionElements.Add("식사", "토");
            actionElements.Add("잤", "물");
            actionElements.Add("잠", "물");
            actionElements.Add("침대", "물");
            actionElements.Add("샀", "금");
            actionElements.Add("사줘", "금");
            actionElements.Add("돈", "금");
            actionElements.Add("일", "불");
            actionElements.Add("일해", "불");
            actionElements.Add("공부", "불");
            actionElements.Add("산책", "목");
            actionElements.Add("운동", "목");
            actionElements.Add("어전", "목");
        }

        public FortuneInterpretation InterpretStory(string story)
        {
            var interpretation = new FortuneInterpretation();
            
            // 이야기에서 오행 요소 분석
            AnalyzeElements(story, interpretation);
            
            // 주요 운세 결정
            DetermineMainElement(interpretation);
            
            // 사주팔자 스타일 해석 생성
            GenerateExplanation(interpretation, story);
            
            return interpretation;
        }

        private void AnalyzeElements(string story, FortuneInterpretation interpretation)
        {
            // 오행 요소 초기화
            interpretation.ElementValues["금"] = 0;
            interpretation.ElementValues["목"] = 0;
            interpretation.ElementValues["물"] = 0;
            interpretation.ElementValues["불"] = 0;
            interpretation.ElementValues["토"] = 0;

            // 감정 분석
            foreach (var emotion in emotionElements)
            {
                if (story.Contains(emotion.Key))
                {
                    interpretation.ElementValues[emotion.Value] += 2;
                }
            }

            // 음식 분석
            foreach (var food in foodElements)
            {
                if (story.Contains(food.Key))
                {
                    interpretation.ElementValues[food.Value] += 3;
                }
            }

            // 시간 분석
            foreach (var time in timeElements)
            {
                if (story.Contains(time.Key))
                {
                    interpretation.ElementValues[time.Value] += 1;
                }
            }

            // 행동 분석
            foreach (var action in actionElements)
            {
                if (story.Contains(action.Key))
                {
                    interpretation.ElementValues[action.Value] += 2;
                }
            }

            // 글자 수에 따른 보정
            int textLength = story.Length;
            string lengthElement = GetElementByNumber(textLength % 5);
            interpretation.ElementValues[lengthElement] += 1;
        }

        private void DetermineMainElement(FortuneInterpretation interpretation)
        {
            interpretation.MainElement = interpretation.ElementValues
                .OrderByDescending(x => x.Value)
                .First().Key;
        }

        private void GenerateExplanation(FortuneInterpretation interpretation, string story)
        {
            var explanation = new System.Text.StringBuilder();
            
            explanation.AppendLine("╭─────── 사주팔자 해석 ───────╮");
            explanation.AppendLine();
            
            // 주요 운세 해석
            explanation.AppendLine(string.Format("☆ 주요 운세: {0}속({1})", interpretation.MainElement, GetElementDescription(interpretation.MainElement)));
            explanation.AppendLine();
            
            // 오행 분석 결과
            explanation.AppendLine("☆ 오행 기운 분석:");
            foreach (var element in interpretation.ElementValues.OrderByDescending(x => x.Value))
            {
                if (element.Value > 0)
                {
                    explanation.AppendLine(string.Format("   {0}속: {1}점 - {2}", element.Key, element.Value, GetElementMeaning(element.Key, element.Value)));
                }
            }
            explanation.AppendLine();
            
            // 운세 해석
            explanation.AppendLine("☆ 운세 해석:");
            explanation.AppendLine(string.Format("   {0}", GetFortuneInterpretation(interpretation.MainElement, story)));
            explanation.AppendLine();
            
            // 로또 번호 의미
            explanation.AppendLine("☆ 로또 번호 의미:");
            explanation.AppendLine(string.Format("   주요 {0}속 기운이 강하여, 이를 바탕으로 한 행운의 숫자를 제시합니다.", interpretation.MainElement));
            
            explanation.AppendLine("╰─────────────────────────────────╯");
            
            interpretation.Explanation = explanation.ToString();
        }

        private string GetElementByNumber(int number)
        {
            string[] elements = { "금", "목", "물", "불", "토" };
            return elements[number % 5];
        }

        private string GetElementDescription(string element)
        {
            var descriptions = new Dictionary<string, string>();
            descriptions.Add("금", "의리와 청렴");
            descriptions.Add("목", "성장과 발전");
            descriptions.Add("물", "지혜와 유연성");
            descriptions.Add("불", "열정과 활력");
            descriptions.Add("토", "안정과 신뢰");
            
            if (descriptions.ContainsKey(element))
                return descriptions[element];
            else
                return "비밀의 운세";
        }

        private string GetElementMeaning(string element, int value)
        {
            if (value >= 5) return "매우 강한 기운";
            if (value >= 3) return "강한 기운";
            if (value >= 1) return "약한 기운";
            return "없음";
        }

        private string GetFortuneInterpretation(string mainElement, string story)
        {
            var interpretations = new Dictionary<string, string[]>();
            
            interpretations.Add("금", new string[] {
                "금속의 기운이 강합니다. 오늘은 의리와 정직이 행운을 가져다주는 날입니다.",
                "금전운이 좋아 예상치 못한 수입이 있을 수 있습니다.",
                "날카로운 판단력으로 중요한 결정을 내리기에 좋은 시기입니다."
            });
            
            interpretations.Add("목", new string[] {
                "목속의 성장 에너지가 강합니다. 새로운 시작과 발전에 좋은 날입니다.",
                "창의적인 아이디어가 떠오르고 새로운 기회가 다가옵니다.",
                "인간관계나 학습에서 좋은 성과를 거둘 수 있습니다."
            });
            
            interpretations.Add("물", new string[] {
                "물속의 지혜와 유연성이 돋보입니다. 직관이 매우 예리한 날입니다.",
                "어려운 문제도 유연한 사고로 해결할 수 있을 것입니다.",
                "휴식과 명상을 통해 내면의 힘을 찾을 수 있는 시기입니다."
            });
            
            interpretations.Add("불", new string[] {
                "불속의 열정과 활력이 넘칩니다. 적극적인 행동이 좋은 결과를 가져다줄 것입니다.",
                "리더십을 발휘하여 주변 사람들에게 좋은 영향을 줄 수 있습니다.",
                "경쟁이나 도전에서 승리할 가능성이 높습니다."
            });
            
            interpretations.Add("토", new string[] {
                "토속의 안정과 신뢰가 두드러집니다. 착실한 노력이 보답받는 날입니다.",
                "가족이나 가까운 사람들과의 인연이 깊어질 것입니다.",
                "장기적인 투자나 계획을 세우기에 좋은 시기입니다."
            });

            string[] elementInterpretations;
            if (interpretations.ContainsKey(mainElement))
                elementInterpretations = interpretations[mainElement];
            else
                elementInterpretations = new string[] { "비밀스러운 운세가 당신을 기다리고 있습니다." };
            
            Random random = new Random(story.GetHashCode());
            return elementInterpretations[random.Next(elementInterpretations.Length)];
        }
    }
}