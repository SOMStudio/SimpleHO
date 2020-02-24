# SimpleHO
Simple Hidden Objects

## Task:
Сделать базовую механику игры в жанре “Hidden objects”:
https://apps.apple.com/us/app/find-hidden-objects-in-picture/id1465691296

Требования:
Собрать игровую сцену с предметами, которые нужно найти.
Сделать верхнюю панель с предметами для поиска на игровой сцене.
По тапу на найденный предмет на сцене, он должен быть подсвечен и анимировано  перемещен на соответствующее место в панеле предметов с любым визуальным эффектом.
Реализовать режим поиска в темноте (затемненный фон, за перемещением пальца должен следовать круглый “просвет” в этом фоне).
Игра должна содержать сцену меню и геймплея.
В сцене геймплея должен быть таймер. По истечению времени (30 сек) игрок проигрывает.
Должен быть попап победы или поражения.
Сохранение очков между сессиями (начисление произвольно).
Игра должна выглядеть адекватно на разных соотношениях сторон экрана.
Использовать шрифт, который находится в папке с assets.

## Implemented:
All

Additionally:
+ Sound, Music
+ TextMeshPro - for text
+ DOTween (free) - for move Ui, objects in game (https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)
+ PathCreator (free) - for move item by spline (https://assetstore.unity.com/packages/tools/utilities/b-zier-path-creator-136082)