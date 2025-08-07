using MyGame.Core;

namespace MyGame.Domain.UseCases
{
    /// <summary>
    /// Manages room progression and flow in a roguelike game.
    /// </summary>
    public class RoomFlow
    {
        private readonly EventBus _eventBus;
        private int _currentRoomIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomFlow"/> class.
        /// </summary>
        /// <param name="eventBus">Global event bus for publishing room events.</param>
        public RoomFlow(EventBus eventBus)
        {
            _eventBus = eventBus;
            _currentRoomIndex = 0;
        }

        /// <summary>
        /// Advances to the next room.
        /// </summary>
        public void GoToNextRoom()
        {
            _currentRoomIndex++;
            _eventBus.Publish(new RoomEnteredEvent(_currentRoomIndex));
        }

        /// <summary>
        /// Gets the index of the current room.
        /// </summary>
        public int CurrentRoomIndex => _currentRoomIndex;
    }

    /// <summary>
    /// Event raised when a new room is entered.
    /// </summary>
    public class RoomEnteredEvent
    {
        /// <summary>
        /// Index of the room entered.
        /// </summary>
        public int RoomIndex { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomEnteredEvent"/> class.
        /// </summary>
        /// <param name="roomIndex">Index of the newly entered room.</param>
        public RoomEnteredEvent(int roomIndex)
        {
            RoomIndex = roomIndex;
        }
    }
}
