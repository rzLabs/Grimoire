using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Tabs.Enums
{
    public class ItemBase
    {
        public enum Type
        {
            ETC = 0,       // 일반
            ARMOR = 1,     // 장비
            CARD = 2,      // 카드
            SUPPLY = 3,        // 소모품
            CUBE = 4,      // 큐브
            CHARM = 5,     // 부적
            USE = 6,       // 사용형 - 소모되지 않고 쿨타임만
            SOULSTONE = 7,     // 소울스톤
            USE_CARD = 8
        };

        public enum Group
        {
            ETC = 0,  // 기타
            WEAPON = 1,   // 무기
            ARMOR = 2,    // 의상
            SHIELD = 3,   // 방패
            HELM = 4, // 투구
            GLOVE = 5,    // 장갑
            BOOTS = 6,    // 부츠
            BELT = 7, // 벨트
            MANTLE = 8,   // 망토
            ACCESSORY = 9,    // 악세서리
            SKILLCARD = 10,   // 스킬 카드
            ITEMCARD = 11,    // 유니트 카드
            SPELLCARD = 12,   // 스펠 카드(미구현)
            SUMMONCARD = 13,  // 소환수 카드
            FACE = 15,    // 가면
            UNDERWEAR = 16,   // 속옷
            BAG = 17, // 가방
            PET_CAGE = 18,    // 펫 우리
            STRIKE_CUBE = 21, // 스트라이크 큐브
            DEFENCE_CUBE = 22,    // 디펜스 큐브
            SKILL_CUBE = 23,  // 스킬 큐브
            RESTORATION_CUBE = 24,    // 환원의 큐브
            SOULSTONE = 93,   // 소울 스톤
            BULLET = 98,  // 탄환
            CONSUMABLE = 99,  // 소모품
            NPC_FACE = 100,   // NPC용 얼굴
            DECO = 110,   // 꾸미기 아이템
            RIDING = 120, // 탑승용 아이템
            ARTIFACT = 130,   // 아티팩트 아이템
            EQUIPMENT_ON_BELT = 140,  // 벨트 장착용 보스 카드
        };

        public enum Class
        {
            // 미분류
            ETC = 0,

            // 무기
            DOUBLE_AXE = 95,      // 쌍도끼
            DOUBLE_SWORD = 96,
            DOUBLE_DAGGER = 98,
            EVERY_WEAPON = 99,
            ETCWEAPON = 100,      // 기타무기
            ONEHAND_SWORD = 101,      // 한손검
            TWOHAND_SWORD = 102,      // 양손검
            DAGGER = 103,     // 단검
            TWOHAND_SPEAR = 104,      // 창
            TWOHAND_AXE = 105,        // 양손도끼
            ONEHAND_MACE = 106,       // 한손메이스
            TWOHAND_MACE = 107,       // 양손메이스
            HEAVY_BOW = 108,      // 헤비 보우
            LIGHT_BOW = 109,      // 라이트 보우
            CROSSBOW = 110,       // 석궁
            ONEHAND_STAFF = 111,      // 한손지팡이
            TWOHAND_STAFF = 112,      // 양손지팡이
            ONEHAND_AXE = 113,        // 한손도끼
                                            //DOUBLE_WEAPON = 199,

            // 방어구
            ARMOR = 200,      // 공용의상
            FIGHTER_ARMOR = 201,      // 전사용
            HUNTER_ARMOR = 202,       // 헌터용
            MAGICIAN_ARMOR = 203,     // 마법사용
            SUMMONER_ARMOR = 204,     // 소환사용

            //
            SHIELD = 210,     // 공용방패
            HELM = 220,       // 공용투구
            BOOTS = 230,      // 공용부츠
            GLOVE = 240,      // 공용장갑
            BELT = 250,       // 공용벨트
            MANTLE = 260,     // 공용망토
            QUIVER = 265,     // 화살통

            // 악세사리
            ETC_ACCESSORY = 300,      // 기타악세사리
            RING = 301,       // 링
            EARRING = 302,        // 귀걸이
            ARMULET = 303,        // 목걸이
            EYEGLASS = 304,       // 안경
            MASK = 305,       // 수염
            CUBE = 306,       // 큐브

            // 포스칩, 소울칩, 루나칩
            BOOST_CHIP = 400,

            // 소울스톤
            SOULSTONE = 401,
            CREATURE_FOOD = 402,
            FARM_PASS = 403,

            // 에테리얼 스톤(충전용 배터리)
            ETHEREAL_STONE = 451,

            // 꾸미기 아이템
            //DECO_WEAPON			= 600,		// 꾸미기 무기, 사용하지 않음(무기 종류별 상세 구분 ID로 대체, 609~621)
            DECO_SHIELD = 601,        // 꾸미기 방패
            DECO_ARMOR = 602,     // 꾸미기 갑옷
            DECO_HELM = 603,      // 꾸미기 투구
            DECO_GLOVE = 604,     // 꾸미기 장갑
            DECO_BOOTS = 605,     // 꾸미기 부츠
            DECO_MALTLE = 606,        // 꾸미기 망토
            DECO_SHOULDER = 607,      // 꾸미기 어깨(날개)
            DECO_HAIR = 608,      // 꾸미기 헤어
            DECO_ONEHAND_SWORD = 609,     // 꾸미기 한손검
            DECO_TWOHAND_SWORD = 610,     // 꾸미기 양손검
            DECO_DAGGER = 611,        // 꾸미기 단검
            DECO_TWOHAND_SPEAR = 612,     // 꾸미기 양손창
            DECO_TWOHAND_AXE = 613,       // 꾸미기 양손도끼
            DECO_ONEHAND_MACE = 614,      // 꾸미기 한손메이스
            DECO_TWOHAND_MACE = 615,      // 꾸미기 양손메이스
            DECO_HEAVY_BOW = 616,     // 꾸미기 헤비 보우
            DECO_LIGHT_BOW = 617,     // 꾸미기 라이트 보우
            DECO_CROSSBOW = 618,      // 꾸미기 석궁
            DECO_ONEHAND_STAFF = 619,     // 꾸미기 한손지팡이
            DECO_TWOHAND_STAFF = 620,     // 꾸미기 양손지팡이
            DECO_ONEHAND_AXE = 621,   // 꾸미기 한손도끼

            ELEMENT_EFFECT = 700,     // 속성이펙트
            ELEMENT_ONEHAND_PHYSICAL = 701,       // 속성이펙트공격력강화 (한손)
            ELEMENT_ONEHAND_MAGICAL = 702,        // 속성이펙트마력강화 (한손)
            ELEMENT_TWOHAND_PHYSICAL = 703,       // 속성이펙트공격력강화 (양손)
            ELEMENT_TWOHAND_MAGICAL = 704     // 속성이펙트마력강화 (양손)
        };

        public enum WearType
        {
            CANTWEAR = -1, // 장비불가
            NONE = -1, // 장비 하지 않음

            WEAPON = 0,    // 오른손전용    (방패라던가)
            SHIELD = 1,    // 왼손전용
            ARMOR = 2, // 갑옷
            HELM = 3,  // 투구
            GLOVE = 4, // 장갑
            BOOTS = 5, // 부츠
            BELT = 6,  // 벨트
            MANTLE = 7,    // 망토
            ARMULET = 8,   // 목걸이
            RING = 9,  // 반지		
                       // 10 은 반지 두번째 슬롯을 위해 예약
            EAR = 11,  // 귀걸이
            FACE = 12, // 눈
            HAIR = 13, // 입
            DECO_WEAPON = 14,
            DECO_SHIELD = 15,
            DECO_ARMOR = 16,
            DECO_HELM = 17,
            DECO_GLOVE = 18,
            DECO_BOOTS = 19,
            DECO_MANTLE = 20,
            DECO_SHOULDER = 21,
            RIDE_ITEM = 22,
            BAG_SLOT = 23,
            SPARE_WEAPON = 24,
            SPARE_SHIELD = 25,
            SPARE_DECO_WEAPON = 26,
            SPARE_DECO_SHIELD = 27,

            TWOFINGER_RING = 94,   // 반지 두칸 소모
            TWOHAND = 99,  // 투핸드

            SKILL = 100,   // 스킬
            SUMMON_ONLY = 200, // 크리처 전용 장비

            SECOND_RING = 10
        }
    }
}