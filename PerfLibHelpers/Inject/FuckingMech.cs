//using System;
//using System.Collections.Generic;
//using BattleTech;
//using BattleTech.Data;
//using HBS;

//namespace Inject
//{
//    namespace BattleTech.Data
//    {
//        internal class LoadTracker
//        {
//            public LoadRequest owner;

//            public VersionManifestEntry resourceManifestEntry;

//            public DataManager.FileLoadRequest backingRequest;

//            public DataManager.DependencyLoadRequest dependencyLoader;

//            public bool completed;

//            public bool allowDuplicateInstantiation;

//            public Action<LoadTracker> callbackContainer;

//            public List<LoadTracker> linkedTrackers = new List<LoadTracker>();

//            public LoadTracker(VersionManifestEntry entry, LoadRequest owner)
//            {
//                this.owner = owner;
//                resourceManifestEntry = entry;
//                completed = false;
//            }
//        }

//        //[ScriptBinding("ContainedLoadRequest")]
//        public class LoadRequest
//        {
//            //            public enum State
//            //            {
//            //                Idle,
//            //                Pending,
//            //                Processing,
//            //                Complete
//            //            }

//                        private static bool debugRequestOrigin = false;

//                        private static HBS.Logging.ILog logger = HBS.Logging.Logger.GetLogger("Data.DataManager.ContainedLoadRequest");

//                        private DataManager dataManager;

//                        private Dictionary<VersionManifestEntry, LoadTracker> loadRequests = new Dictionary<VersionManifestEntry, LoadTracker>();

//            //            private List<LoadTracker> pendingRequests = new List<LoadTracker>();

//            //            private List<LoadTracker> linkedPendingRequests = new List<LoadTracker>();

//            //            private List<LoadTracker> processingRequests = new List<LoadTracker>();

//            //            private List<LoadTracker> completedRequests = new List<LoadTracker>();

//            //            private List<VersionManifestEntry> failedRequests = new List<VersionManifestEntry>();

//            //            private Action<LoadRequest> loadCompleteCallback;

//                        private bool filterByOwnershipDefault;

//            //            private StackTrace debugStackTrace;

//                        public State CurrentState { get; private set; }

//            //            public List<VersionManifestEntry> FailedRequests => new List<VersionManifestEntry>(failedRequests);

//            //            internal Dictionary<VersionManifestEntry, LoadTracker> AllLoadRequests => loadRequests;

//            //            internal LoadTracker this[VersionManifestEntry entry]
//            //            {
//            //                get
//            //                {
//            //                    if (entry == null)
//            //                    {
//            //                        return null;
//            //                    }
//            //                    try
//            //                    {
//            //                        return loadRequests[entry];
//            //                    }
//            //                    catch
//            //                    {
//            //                        return null;
//            //                    }
//            //                }
//            //            }

//            //            internal LoadRequest(DataManager dataManager)
//            //            {
//            //                this.dataManager = dataManager;
//            //                loadCompleteCallback = null;
//            //                filterByOwnershipDefault = false;
//            //                CurrentState = State.Idle;
//            //                if (debugRequestOrigin)
//            //                {
//            //                    debugStackTrace = new StackTrace();
//            //                }
//            //            }

//            //            internal LoadRequest(DataManager dataManager, Action<LoadRequest> loadCompleteCallback = null, bool filterByOwnership = false)
//            //            {
//            //                this.dataManager = dataManager;
//            //                this.loadCompleteCallback = loadCompleteCallback;
//            //                filterByOwnershipDefault = filterByOwnership;
//            //                CurrentState = State.Idle;
//            //                if (debugRequestOrigin)
//            //                {
//            //                    debugStackTrace = new StackTrace();
//            //                }
//            //            }

//            //            internal void Init(Action<LoadRequest> loadCompleteCallback = null, bool filterByOwnership = false)
//            //            {
//            //                this.loadCompleteCallback = loadCompleteCallback;
//            //                filterByOwnershipDefault = filterByOwnership;
//            //                CurrentState = State.Idle;
//            //                if (debugRequestOrigin)
//            //                {
//            //                    debugStackTrace = new StackTrace();
//            //                }
//            //            }

//            //            internal LoadTracker GetLoadTracker(BattleTechResourceType type, string resourceId)
//            //            {
//            //                VersionManifestEntry versionManifestEntry = dataManager.ResourceLocator.EntryByID(resourceId, type);
//            //                if (versionManifestEntry != null)
//            //                {
//            //                    return this[versionManifestEntry];
//            //                }
//            //                return null;
//            //            }

//            //            public int GetRequestCount()
//            //            {
//            //                return loadRequests.Count;
//            //            }


//            //            public void AddAllOfTypeBlindLoadRequest(BattleTechResourceType resourceType, bool? filterByOwnerShip = false)
//            //            {
//            //                VersionManifestEntry[] array = dataManager.ResourceLocator.AllEntriesOfResource(resourceType);
//            //                foreach (VersionManifestEntry versionManifestEntry in array)
//            //                {
//            //                    if (!versionManifestEntry.IsTemplate)
//            //                    {
//            //                        AddBlindLoadRequestInternal(resourceType, versionManifestEntry.Id, allowDuplicateInstantiation: false, filterByOwnerShip);
//            //                    }
//            //                }
//            //            }

//            //            public void AddBlindLoadRequest(BattleTechResourceType resourceType, string resourceId, bool? filterByOwnership = false)
//            //            {
//            //                AddBlindLoadRequestInternal(resourceType, resourceId, allowDuplicateInstantiation: false, filterByOwnership);
//            //            }

//            //            public void AddBlindLoadRequestAllowStacking(BattleTechResourceType resourceType, string resourceId, bool? filterByOwnership = false)
//            //            {
//            //                AddBlindLoadRequestInternal(resourceType, resourceId, allowDuplicateInstantiation: true, filterByOwnership);
//            //            }

//            //            public void AddPrewarmRequest(PrewarmRequest prewarm)
//            //            {
//            //                if (prewarm.PrewarmAllOfType)
//            //                {
//            //                    VersionManifestEntry[] array = dataManager.ResourceLocator.AllEntriesOfResource(prewarm.ResourceType);
//            //                    foreach (VersionManifestEntry versionManifestEntry in array)
//            //                    {
//            //                        prewarm.OverrideResourceID(versionManifestEntry.Id);
//            //                        AddBlindLoadRequestInternal(prewarm.ResourceType, prewarm.ResourceID, allowDuplicateInstantiation: true, true);
//            //                    }
//            //                }
//            //                else
//            //                {
//            //                    AddBlindLoadRequestInternal(prewarm.ResourceType, prewarm.ResourceID, allowDuplicateInstantiation: true, true);
//            //                }
//            //            }


//            //            private void AddBlindLoadRequestInternal(BattleTechResourceType resourceType, string resourceId, bool allowDuplicateInstantiation, bool? filterByOwnerShip = false)
//            //            {
//            //                LoadTracker tracker = null;
//            //                if (TryCreateAndAddLoadRequest(out tracker, resourceType, resourceId, filterByOwnerShip))
//            //                {
//            //                    tracker.callbackContainer = null;
//            //                    tracker.allowDuplicateInstantiation = allowDuplicateInstantiation;
//            //                }
//            //            }

//            private bool TryCreateAndAddLoadRequest(out LoadTracker tracker, BattleTechResourceType resourceType, string resourceId, bool? filterByOwnership)
//            {
//                tracker = null;
//                if (string.IsNullOrEmpty(resourceId))
//                {
//                    logger.LogError($"Request of Type {resourceType} was given an empty Identifier");
//                    return false;
//                }
//                VersionManifestEntry versionManifestEntry = dataManager.ResourceLocator.EntryByID(resourceId, resourceType);
//                logger.Log($"{resourceId}");
//                if (versionManifestEntry == null)
//                {
//                    logger.LogError($"ManifestEntry is null for [{resourceId}] [{resourceType}] - resourceIds are case sensitive, so be sure to check that too");
//                    return false;
//                }
//                bool value = filterByOwnershipDefault;
//                if (filterByOwnership.HasValue)
//                {
//                    value = filterByOwnership.Value;
//                }
//                bool flag = !value || dataManager.ContentPackIndex.IsResourceOwned(versionManifestEntry.Id);
//                if (versionManifestEntry.IsTemplate || !flag)
//                {
//                    return false;
//                }
//                tracker = new LoadTracker(versionManifestEntry, this);
//                loadRequests[versionManifestEntry] = tracker;
//                CurrentState = State.Pending;
//                return true;
//            }
//        }
//    }
//}


////            internal int GetPendingRequestCount()
////            {
////                return pendingRequests.Count + linkedPendingRequests.Count;
////            }

////            internal int GetActiveRequestCount()
////            {
////                return processingRequests.Count;
////            }

////            internal int GetActiveHeavyRequestCount()
////            {
////                int num = 0;
////                for (int i = 0; i < processingRequests.Count; i++)
////                {
////                    LoadTracker loadTracker = processingRequests[i];
////                    if (loadTracker.backingRequest.State == DataManager.FileLoadRequest.RequestState.Processing && loadTracker.backingRequest.RequestWeight.AllowedWeight >= 1000)
////                    {
////                        num++;
////                    }
////                }
////                return num;
////            }

////            internal int GetActiveLightRequestCount()
////            {
////                int num = 0;
////                for (int i = 0; i < processingRequests.Count; i++)
////                {
////                    LoadTracker loadTracker = processingRequests[i];
////                    if (loadTracker.backingRequest.State == DataManager.FileLoadRequest.RequestState.Processing && loadTracker.backingRequest.RequestWeight.AllowedWeight <= 10)
////                    {
////                        num++;
////                    }
////                }
////                return num;
////            }

////            internal int GetCompletedRequestCount()
////            {
////                return completedRequests.Count;
////            }

////            internal DataManager.FileLoadRequest PopPendingRequest()
////            {
////                if (pendingRequests.Count == 0)
////                {
////                    return null;
////                }
////                LoadTracker loadTracker = pendingRequests[0];
////                pendingRequests.RemoveAt(0);
////                processingRequests.Add(loadTracker);
////                return loadTracker.backingRequest;
////            }

////            internal void TryHandleLoadFailed(DataManager.FileLoadRequest fileRequest)
////            {
////                for (int i = 0; i < processingRequests.Count; i++)
////                {
////                    LoadTracker loadTracker = processingRequests[i];
////                    if (!loadTracker.completed)
////                    {
////                        if (loadTracker.backingRequest == fileRequest)
////                        {
////                            CompleteLoadTracker(loadTracker, loadSuccess: false);
////                        }
////                        else if (loadTracker.dependencyLoader != null && loadTracker.dependencyLoader.BackingRequest == fileRequest)
////                        {
////                            loadTracker.backingRequest.NotifyLoadFailed();
////                        }
////                    }
////                }
////            }


////            private void CompleteLoadTracker(LoadTracker tracker, bool loadSuccess)
////            {
////                tracker.completed = true;
////                if (loadSuccess)
////                {
////                    if (tracker.callbackContainer != null)
////                    {
////                        tracker.callbackContainer(tracker);
////                    }
////                    for (int i = 0; i < tracker.linkedTrackers.Count; i++)
////                    {
////                        LoadTracker loadTracker = tracker.linkedTrackers[i];
////                        loadTracker.backingRequest = tracker.backingRequest;
////                        loadTracker.dependencyLoader = tracker.dependencyLoader;
////                        loadTracker.owner.CompleteLoadTracker(loadTracker, loadSuccess);
////                    }
////                }
////                else
////                {
////                    failedRequests.Add(tracker.resourceManifestEntry);
////                }
////                for (int j = 0; j < pendingRequests.Count; j++)
////                {
////                    if (pendingRequests[j] == tracker)
////                    {
////                        pendingRequests.RemoveAt(j);
////                        break;
////                    }
////                }
////                for (int k = 0; k < linkedPendingRequests.Count; k++)
////                {
////                    if (linkedPendingRequests[k] == tracker)
////                    {
////                        linkedPendingRequests.RemoveAt(k);
////                        break;
////                    }
////                }
////                for (int l = 0; l < processingRequests.Count; l++)
////                {
////                    if (processingRequests[l] == tracker)
////                    {
////                        processingRequests.RemoveAt(l);
////                        break;
////                    }
////                }
////                completedRequests.Add(tracker);
////            }


////            internal void Reset()
////            {
////                CurrentState = State.Idle;
////                loadCompleteCallback = null;
////                loadRequests.Clear();
////                pendingRequests.Clear();
////                linkedPendingRequests.Clear();
////                processingRequests.Clear();
////                completedRequests.Clear();
////                failedRequests.Clear();
////            }
////        }
////    }
////}
